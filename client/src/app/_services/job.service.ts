import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { listingForm } from '../models/listingform';
import { User } from '../models/user';
import { JobParams } from '../models/jobParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Job } from '../models/job';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  baseUrl = environment.apiUrl;
  user: User | undefined;
  jobParams: JobParams | undefined;
  jobCache = new Map();

  constructor(private http: HttpClient, private accountService: AccountService) {
    // calls the current user, not sure if i need this yet
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user
        console.log(user);
      }
    })
   }

   createListing(model: listingForm) {
    model.username = this.user.username
    console.log(model);
     return this.http.post(this.baseUrl + 'joblistings/createlisting', model)
   }

   getJobParams() {
    return this.jobParams
   }

   setJobParams(params: JobParams) {
    this.jobParams = params;
   }

   getJobs(jobParams: JobParams)
   {  // Need to create a new API controller to handle pagination 
      const response = this.jobCache.get(Object.values(jobParams).join('-'));
      if (response) return of (response)

      let params = getPaginationHeaders(jobParams.pageNumber, jobParams.pageSize);

      return getPaginatedResult<listingForm[]>(this.baseUrl + 'joblistings', params, this.http).pipe(
        map(response => {
          this.jobCache.set(Object.values(jobParams).join('-'), response)
          return response;
        })
      )
   }

   // ID needs to be a number i changed it for the sake of not showing an error
   getJob(id: string) {
    const job = [...this.jobCache.values()]
    .reduce((arr, elem) => arr.concat(elem.result), [])
    .find((job: Job) => job.username === id);

    if (job) return of (job);

    return this.http.get<Job>(this.baseUrl + 'joblistings/' + id);
   }

   deleteJob(id: string) {
    return this.http.post(this.baseUrl + 'joblistings/delete', id)
   }

   updateJob(id: string) {
    return this.http.put(this.baseUrl + 'joblistings/update', id)
   }
}
