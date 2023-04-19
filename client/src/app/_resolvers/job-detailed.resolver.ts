import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Job } from '../models/job';
import { JobService } from '../_services/job.service';

@Injectable({
  providedIn: 'root'
})
export class JobDetailedResolver implements Resolve<Job> {
  constructor(private jobService: JobService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Job> {
    return this.jobService.getJob(route.paramMap.get('id')!);
  }
}
