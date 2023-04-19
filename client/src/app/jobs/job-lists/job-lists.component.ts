import { Component, OnInit } from '@angular/core';
import { JobParams } from 'src/app/models/jobParams';
import { Pagination } from 'src/app/models/pagination';
import { JobService } from 'src/app/_services/job.service';
import { Job } from 'src/app/models/job';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-job-lists',
  templateUrl: './job-lists.component.html',
  styleUrls: ['./job-lists.component.css']
})
export class JobListsComponent implements OnInit {
  jobs$: Observable<Job[]> | undefined;
  jobs: Job[] = [];
  pagination: Pagination | undefined;
  jobParams: JobParams = new JobParams;
  constructor(private jobService: JobService ) {

   }

  ngOnInit(): void {
    this.LoadJobs()
  }

  LoadJobs() {
    //console.log(this.jobParams);
    if (this.jobParams) {
      this.jobService.setJobParams(this.jobParams);
      this.jobService.getJobs(this.jobParams).subscribe({
        next: response => {
          if (response.result && response.pagination ) {
            this.jobs = response.result;
            console.log(this.jobs)
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  pageChanged(event: any) {
    if(this.jobParams && this.jobParams.pageNumber !== event.page){
      this.jobParams.pageNumber = event.page;
      this.jobService.setJobParams(this.jobParams);
      this.LoadJobs();
    }
    
  }

}
 