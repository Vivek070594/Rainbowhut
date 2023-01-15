import { Component, OnInit, ElementRef, AfterViewInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { SharedservicesService } from './services/sharedservices.service';
import { ContactUs, FileModel } from './models/model';
import { AlertifyService } from './services/alertify-service.service';
import { interval, take } from 'rxjs';





@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  contactus: ContactUs={
    name:'',
    email:'',
    subject:'',
    message:''
    
  };

  enteredotp:string='';
  loading = false;
  imagezoomgallery=false;
  adminLoginbtn=true;
  adminLogin=false;
  adminsecurity=false;
  resendotp=false;
  downloadbtn=false;
  currentYear = new Date().getFullYear();
  Experinced_year=this.currentYear-2017;
  imageprofile:any=[];
  imageslideshow:any=[];
  imagegallery:any=[];
  Iimageprofile:any=[];
  Iimageslideshow:any=[];
  Iimagegallery:any=[];
  galleryimage:string='';
  time:string='';
  otptimer=false;
  qrcode:any=[];
  gallerytypename:string='All';
  constructor(private sharedservice:SharedservicesService,private alertify:AlertifyService,private elementRef: ElementRef) {
   
   }
  ngOnInit(): void {
    // this.GetAllImage();
    
  }
//Sendmail

SendMail(){
  this.loading = true;
   this.sharedservice.SendMail(this.contactus).subscribe(
     (     response: { toString: () => string; })=>{
      if(response.toString()=="Success")
      {
        this.loading = false;
      this.alertify.success("Your query submitted successfully.We will response you shortly.");
      this.contactus={
        name:'',
        email:'',
        subject:'',
        message:''
      }
      }
      else{
        this.loading = false;
        this.alertify.error("Sorry,We have some problem in server.Please contact through whatsapp.");
      }
    }
   );

}
GetOtp(){
  this.loading = true;
  this.sharedservice.GetOtp().subscribe(
    response=>{
 if(response.toString()!="Failed")
 {
   this.sharedservice.SetOtp(response.toString());
        this.startTimer();
        this.adminLoginbtn=false;
  this. adminLogin=true;
        this.otptimer=true;
        this.resendotp=false;
        this.loading = false;
        this.GetProfileImage();
        this.GetSlideShowImage();
        this.GetGalleryImage();
   
 }
 else{
   this.loading = false;
 }
}
);
}
startTimer() {
  var timer =120;
  var minutes;
  var seconds;

  interval(1000).pipe(take(121)).subscribe(x => {
    
      minutes = Math.floor(timer / 60);
      seconds = Math.floor(timer % 60);

      minutes = minutes < 10 ? "0" + minutes : minutes;
      seconds = seconds < 10 ? "0" + seconds : seconds;

      this.time = minutes + ":" + seconds;

    
      if (timer== 0) {
           this.sharedservice.ClearOtp();
           this.resendotp=true;
           this.otptimer=false;
      }
      else if(timer>0){
        --timer;
      }
  })
}
SendOTP(){
  this.loading = true;

  let entered=this.enteredotp;
  if(entered!=""){
  let originalotp=this.sharedservice.GetStoredOtp();
  if(entered==originalotp){
    this.adminLoginbtn=false;
    this. adminLogin=false;
    this. adminsecurity=true;
    this.loading = false;
    this.sharedservice.ClearOtp();
  }
  else{
    this.loading = false;
    this.alertify.error("Invalid OTP");
  }
}
}
profileImage(event:any) {
  let file: File = event.target.files[0];
  const formData = new FormData();
    formData.append('file', file, file.name);
    this.sharedservice.SetFormData(formData);
 
}
profileImageUpload(){
  this.loading = true;
  let formdata=this.sharedservice.GetFormData();
  this.sharedservice.UploadProfileImage(formdata).subscribe(
    (     response: { toString: () => string; })=>{
     if(response.toString()=="Success")
     {
       this.loading = false;
        this.GetProfileImage();
     this.alertify.success("Profile Image Uploaded successfully.");
    
     }
     else{
       this.loading = false;
       this.alertify.error("Sorry,We have some problem in server.Please contact Administrator.");
     }
   }
  );
 
}
slideshowImage(event:any) {
  let file: File = event.target.files[0];
  const formData = new FormData();
    formData.append('file', file, file.name);
    this.sharedservice.SetFormData(formData);
 
}
slideshowImageUpload(){
  this.loading = true;
  let formdata=this.sharedservice.GetFormData();
  this.sharedservice.UploadSlideshowImage(formdata).subscribe(
    (     response: { toString: () => string; })=>{
     if(response.toString()=="Success")
     {
       this.loading = false;
       this.GetSlideShowImage();
     this.alertify.success("Image Uploaded successfully.");
    
     }
     else{
       this.loading = false;
       this.alertify.error("Sorry,We have some problem in server.Please contact Administrator.");
     }
   }
  );
 
}
selectChangeHandler (event: any) {
  this.sharedservice.SetGalleryType(event.target.value);
}
galleryImage(event:any) {
  let file: File = event.target.files[0];
  
    this.sharedservice.SetFileData(file);
}
galleryImageUpload(){
  this.loading = true;
  let file=this.sharedservice.GetFileData();
  let type=this.sharedservice.GetGalleryType();
  const formData = new FormData();
    formData.append(type, file, file.name);
  this.sharedservice.UploadGalleryImage(formData).subscribe(
    (     response: { toString: () => string; })=>{
     if(response.toString()=="Success")
     {
       this.loading = false;
        this.GetGalleryImage();
     this.alertify.success("Image Uploaded successfully.");
    
     }
     else{
       this.loading = false;
       this.alertify.error("Sorry,We have some problem in server.Please contact Administrator.");
     }
   }
  );
 
}

QrCodeFile(event:any) {
  let file: File = event.target.files[0];
  const formData = new FormData();
    formData.append('file', file, file.name);
    this.sharedservice.SetFormData(formData);
 
}
QrCodeFileUpload(){
   this.loading = true;
  let formdata=this.sharedservice.GetFormData();
  this.sharedservice.QrCodeFileUpload(formdata).subscribe(
    (     response: { toString: () => string; })=>{
     if(response.toString()!="Failed")
     {
        this.loading = false;
       this.qrcode=response;
       this.downloadbtn=true;
    
     }
     else{
        this.loading = false;
       this.alertify.error("Sorry,We have some problem in server.Please contact Administrator.");
     }
   }
  );
 
}

GetProfileImage(){
  this.loading = true;
   this.sharedservice.GetProfileImage().subscribe(
         response=>{
          console.log(response);
      if(response.toString()!="Failed")
      {
        this.loading = false;
        this.imageprofile=Object.values(response);
      }
      else{
        this.loading = false;
      }
    }
   );

}
GetSlideShowImage(){
  this.loading = true;
   this.sharedservice.GetSlideShowImage().subscribe(
         response=>{
      if(response.toString()!="Failed")
      {
        this.loading = false;
        this.imageslideshow=Object.values(response);
      }
      else{
        this.loading = false;
      }
    }
   );

}
GetGalleryImage(){
  this.loading = true;
   this.sharedservice.GetGalleryImage().subscribe(
         response=>{
      if(response.toString()!="Failed")
      {
        this.loading = false;
        this.imagegallery=Object.values(response);
        this.sharedservice.SetGalleryImage(this.imagegallery);
      }
      else{
        this.loading = false;
      }
    }
   );

}
GetAllImage(){
  this.loading = true;
   this.sharedservice.GetAllImage().subscribe(
         response=>{
          console.log(response);
      if(response.toString()!="Failed")
      {
        this.loading = false;
        this.Iimageprofile=response.profilemodel;
        this.Iimageslideshow=response.slideshowmodel;
        this.Iimagegallery=response.gallerymodel;
      }
      else{
        this.loading = false;
      }
    }
   );

}
AllGallery(){
this.imagegallery=this.sharedservice.GetGalleryImagess();
this.gallerytypename='All';
}
WeddingGallery(){
  let newimage=[];
  newimage=this.sharedservice.GetGalleryImagess();
  this.imagegallery=newimage.filter((x: { galleryType: string; })=>x.galleryType=="Wedding");
  this.gallerytypename='Wedding';
}
ModelGallery(){
  let newimage=[];
  newimage=this.sharedservice.GetGalleryImagess();
  this.imagegallery=newimage.filter((x: { galleryType: string; })=>x.galleryType=="Model");
  this.gallerytypename='Model';
}
EventGallery(){
  let newimage=[];
  newimage=this.sharedservice.GetGalleryImagess();
  this.imagegallery=newimage.filter((x: { galleryType: string; })=>x.galleryType=="Events");
  this.gallerytypename='Events';
}
VideoGallery(){
  let newimage=[];
  newimage=this.sharedservice.GetGalleryImagess();
  this.imagegallery=newimage.filter((x: { galleryType: string; })=>x.galleryType=="Video");
  this.gallerytypename='Video';
}
deleteRowSlideShow(d:any){
  this.sharedservice.deleteRowSlideShow(d).subscribe(
    (     response: { toString: () => string; })=>{
     if(response.toString()=="Success")
     {
      this.GetSlideShowImage();
       this.loading = false;
      
     this.alertify.success("Image Deleted successfully.");
    
     }
     else{
       this.loading = false;
       this.alertify.error("Sorry,We have some problem in server.");
     }
   }
  );
}
deleteRowGallery(d:any){
  this.sharedservice.deleteRowGallery(d).subscribe(
    (     response: { toString: () => string; })=>{
     if(response.toString()=="Success")
     {
      this.GetGalleryImage()
       this.loading = false;
     this.alertify.success("Image Deleted successfully.");
    
     }
     else{
       this.loading = false;
       this.alertify.error("Sorry,We have some problem in server.");
     }
   }
  );
}
galleryClick(data:any)
{
  this.galleryImage=data;
  this.imagezoomgallery=true;
}
QrCodeDownload(qrcode:any)
{
  const downloadLink = document.createElement('a');
    const fileName = qrcode.qrcode.fileName+".jpg";
    downloadLink.href = qrcode.qrcode.data;
    downloadLink.download = fileName;
    downloadLink.click();
}

  //owl carousel
  customOptions: OwlOptions = {
    loop: true,
    mouseDrag: false,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 600,
    navText: ['&#8249', '&#8250;'],
    autoplay:true,
    autoplayTimeout:1000,
    autoHeight:false,
    autoplayHoverPause:false,
    responsive: {
      0: {
        items: 1 
      },
      320: {
        items: 2
      },
      480: {
        items: 3
      },
      640: {
        items: 4
      },
      900: {
        items: 5
      },
      1060: {
        items: 6
      },
      1120: {
        items: 7
      },
      1280: {
        items: 8
      },
      1440: {
        items: 9
      },
      1600: {
        items: 10
      },
      1760: {
        items: 11
      },
      1920: {
        items: 12
      },
      2080: {
        items: 13
      }
    },
    nav: false
  }
  customOptions1: OwlOptions = {
    loop: true,
    rtl:true,
    mouseDrag: false,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 600,
    navText: ['&#8249', '&#8250;'],
    autoplay:true,
    autoplayTimeout:1000,
    autoHeight:false,
    autoplayHoverPause:false,
    responsive: {
      0: {
        items: 1 
      },
      320: {
        items: 2
      },
      480: {
        items: 3
      },
      640: {
        items: 4
      },
      900: {
        items: 5
      },
      1060: {
        items: 6
      },
      1120: {
        items: 7
      },
      1280: {
        items: 8
      },
      1440: {
        items: 9
      },
      1600: {
        items: 10
      },
      1760: {
        items: 11
      },
      1920: {
        items: 12
      },
      2080: {
        items: 13
      }
    },
    nav: false
  }

  title = 'Ui';


}
function convertDataURIToBinary(dataURI:any) {
  var base64Index = dataURI.indexOf(';base64,') + ';base64,'.length;
  var base64 = dataURI.substring(base64Index);
  var raw = window.atob(base64);
  var rawLength = raw.length;
  var array = new Uint8Array(new ArrayBuffer(rawLength));

  for(let i = 0; i < rawLength; i++) {
    array[i] = raw.charCodeAt(i);
  }
  return array;
}

