import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Job } from 'src/app/models/job';
import { Message } from 'src/app/models/message';
import { JobService } from 'src/app/_services/job.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-job-detail',
  templateUrl: './job-detail.component.html',
  styleUrls: ['./job-detail.component.css']
})
export class JobDetailComponent implements OnInit {
  job: Job;
  activeTab: TabDirective;
  messages: Message[] = [];
  
  constructor(private jobService: JobService, private route: ActivatedRoute, private messageService: MessageService) { }
  
  ngOnInit(): void {
    this.route.data.subscribe({
      next: data => this.job = data['job']
    })
    console.log(this.job);
    
   
  }

  
}
