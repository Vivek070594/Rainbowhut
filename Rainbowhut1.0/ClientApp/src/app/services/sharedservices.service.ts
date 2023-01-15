import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ContactUs, FileModel } from '../models/model';


@Injectable({
  providedIn: 'root'
})
export class SharedservicesService {
  readonly apiurl="https://localhost:7064/api/";
  bytearray:any=[];
  type:string='';
  datafile!: File;
  otp:string='';
  storeimagegallery:any=[];

  constructor(private http:HttpClient) { }
  //Send mail
  SendMail(contact:ContactUs){
    return this.http.post(this.apiurl+'Mail/SendEmail',contact )
  }
  GetOtp(){
    return this.http.get(this.apiurl+'Mail/GetOtp', )
  }
  SetOtp(otpdata:string)
  {
this.otp=otpdata;
  }
  GetStoredOtp(){
    return this.otp;
  }
  ClearOtp()
  {
    this.otp='';
  }
  SetGalleryType(data:any){
    this.type=data;
  }
  GetGalleryType(){
    return this.type;
  }
  SetFormData(data:any){
    this.bytearray=data;
  }
  GetFormData(){
    return this.bytearray;
  }
  SetFileData(data:File){
    this.datafile=data;
  }
  GetFileData(){
    return this.datafile;
  }
  SetGalleryImage(data:any){
this.storeimagegallery=data;
  }
  GetGalleryImagess(){
    return this.storeimagegallery;
  }
  UploadProfileImage(formData:FormData){
    return this.http.post(this.apiurl+'UploadData/ProfileImageUpdate',formData,{reportProgress: true} )
  }
  UploadSlideshowImage(formData:FormData){
    return this.http.post(this.apiurl+'UploadData/SlideImageAdd',formData,{reportProgress: true} )
  }
  UploadGalleryImage(formData:FormData){
    return this.http.post(this.apiurl+'UploadData/GalleryImageAdd',formData ,{reportProgress: true} )
  }
  GetProfileImage(){
    return this.http.get(this.apiurl+'UploadData/GetProfileImage' )
  }
  GetSlideShowImage(){
    return this.http.get(this.apiurl+'UploadData/GetSlideShowImage' )
  }
  GetGalleryImage(){
    return this.http.get(this.apiurl+'UploadData/GetGalleryImage' )
  }
  GetAllImage():Observable<FileModel>{
    return this.http.get<FileModel>(this.apiurl+'UploadData/GetAllImage' )
  }

  deleteRowSlideShow(id:any){
    return this.http.post(this.apiurl+'UploadData/SlideImageDelete',id )
  }
  deleteRowGallery(id:any){
    return this.http.post(this.apiurl+'UploadData/GalleryImageDelete',id )
  }
  QrCodeFileUpload(formData:FormData){
    return this.http.post(this.apiurl+'UploadData/QrCodeFileAdd',formData,{reportProgress: true} )
  }
  
}
