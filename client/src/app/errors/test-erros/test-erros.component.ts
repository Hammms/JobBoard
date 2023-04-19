import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-test-erros',
  templateUrl: './test-erros.component.html',
  styleUrls: ['./test-erros.component.css']
})
export class TestErrosComponent implements OnInit {
  BaseUrl = 'https://localhost:5001/api/'
  validationErrors: string[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  get404Error() {
    this.http.get(this.BaseUrl + 'debugger/not-found').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get400Error() {
    this.http.get(this.BaseUrl + 'debugger/bad-request').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }

  get500Error() {
    this.http.get(this.BaseUrl + 'debugger/server-error').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }


  get401Error() {
    this.http.get(this.BaseUrl + 'debugger/auth').subscribe({
      next: response => console.log(response),
      error: error => console.log(error)
    })
  }


    get400ValidationError() {
      this.http.post(this.BaseUrl + 'account/register', {}).subscribe({
        next: response => console.log(response),
        error: error => {
          console.log(error)
          this.validationErrors = error;
        }
      })
  }



  // .subscribe({
  //   next: response => this.router.navigateByUrl('/members'),
  //   error: error => { console.log(error); this.toastr.error(error.error); }
  // })



}
