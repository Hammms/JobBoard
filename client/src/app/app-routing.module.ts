import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { combineLatest } from 'rxjs';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { TestErrosComponent } from './errors/test-erros/test-erros.component';
import { HomeComponent } from './home/home.component';
import { JobDetailComponent } from './jobs/job-detail/job-detail.component';
import { JobListingformComponent } from './jobs/joblistingform/joblistingform.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListsComponent } from './members/member-lists/member-lists.component';
import { MessagesComponent } from './messages/messages.component';
import { RegisterComponent } from './register/register.component';
import { StripeCheckoutComponent } from './stripe-checkout/stripe-checkout.component';
import { JobEditComponent } from './jobs/job-edit/job-edit.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { JobDetailedResolver } from './_resolvers/job-detailed.resolver';
import { MemberDetailedResolver } from './_resolvers/member-detailed.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { AdminGuard } from './_guards/admin.guard';

const routes: Routes = [
  // Anyone Can access 
  {path: '', component: HomeComponent},
  
  // User Authenticaton Routes
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {path: 'members', component: MemberListsComponent},
      {path: 'members/:username', component: MemberDetailComponent, resolve: {member: MemberDetailedResolver}},
      {path: 'jobs/:id', component: JobDetailComponent, resolve: {job: JobDetailedResolver}},
      {path: 'member/edit', component: MemberEditComponent, canDeactivate: [PreventUnsavedChangesGuard]},
      {path: 'lists', component: ListsComponent},
      {path: 'messages', component: MessagesComponent},
      {path: 'checkout', component: StripeCheckoutComponent},
      {path: 'jobpost', component: JobListingformComponent},
      {path: 'jobdetail', component: JobDetailComponent},
      {path: 'jobedit', component: JobEditComponent},
      {path: 'admin', component: AdminPanelComponent, canActivate: [AdminGuard]}
    ] 
  },
  {path: 'register', component: RegisterComponent},
  {path: 'errors', component: TestErrosComponent},
  {path: 'not-found', component: NotFoundComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: '**', component: NotFoundComponent, pathMatch: 'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
