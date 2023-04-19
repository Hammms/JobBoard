import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css']
})
export class RolesModalComponent implements OnInit {
  username = '';
  availableRoles: any[] =[];
  selectedRoels: any[] = [];

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit(): void {
  }

  updateSelectedRoles(checkedValue: any) {
    const index = this.selectedRoels.indexOf(checkedValue);
    index !== -1 ? this.selectedRoels.splice(index, 1) : this.selectedRoels.push(checkedValue);
  }

}
