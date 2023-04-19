import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileUploader } from 'ng2-file-upload';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { listingForm } from '../../models/listingform';
import { Member } from '../../models/member';
import { User } from '../../models/user';
import { JobService } from '../../_services/job.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-joblistingform',
  templateUrl: './joblistingform.component.html',
  styleUrls: ['./joblistingform.component.css']
})
export class JobListingformComponent implements OnInit {
  @Input() member: Member | undefined;
  jobForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;
  constructor( private toastr: ToastrService, private fb: FormBuilder, private jobService: JobService, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.jobForm = this.fb.group ({
      CompanyName: ['', [Validators.required]],
      ListingImage: [''],
      JobTitle: ['', [Validators.required]],
      Location: ['', [Validators.required]],
      JobType: ['', [Validators.required]],
      Description: ['', [Validators.required]],
      Qualifications: ['', [Validators.required]],
    });
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.user?.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    })

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }
  
    this.uploader.onSuccessItem = (item, response, status, headers) => {
       const photo = JSON.parse(response);
       this.jobForm.setValue({ListingImage: photo.url})
    }
  }

  fileOverBase(e: any){
    this.hasBaseDropZoneOver = e;
  }

  async submit() {
    //this.uploader.uploadAll();
    const values = {...this.jobForm.value};
    console.log(values);
    this.jobService.createListing(values).subscribe({
      next: () => {
        this.router.navigateByUrl('/')
      }
    });
  }

  cancel() {
    
  }

  
}
