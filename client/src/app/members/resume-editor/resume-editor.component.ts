import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { User } from 'src/app/models/user';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-resume-editor',
  templateUrl: './resume-editor.component.html',
  styleUrls: ['./resume-editor.component.css']
})
export class ResumeEditorComponent implements OnInit {
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;
  constructor() { }

  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any){
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'resume/resumeupload',
    })

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false
    }
  
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      const resume = JSON.parse(response);
      
    }

  }

}
