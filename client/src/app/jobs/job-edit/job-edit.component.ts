import { Component, OnInit } from '@angular/core';
import { QuillModules} from 'ngx-quill';
import { Quill } from 'quill';

@Component({
  selector: 'app-job-edit',
  templateUrl: './job-edit.component.html',
  styleUrls: ['./job-edit.component.css']
})
export class JobEditComponent implements OnInit {
  editorModules = {};
  content = '';

  constructor() {
    this.editorModules = {
      toolbar: [
        ['bold', 'italic', 'underline', 'strike'],
        [{ 'header': 1 }, { 'header': 2 }],
        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
        [{ 'align': [] }],
        ['link', 'image']
      ]
    };
  }
  ngOnInit(): void {
  }

 

}
