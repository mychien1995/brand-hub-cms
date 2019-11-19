import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base.component';
import { ConfigService } from '../../services/shared/config.service';
import { ClientService } from '../../services/shared/client.service';
import { OrganizationModel } from '../../models/organizations/organization.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-organization-edit',
  templateUrl: './organization-edit.component.html',
  styleUrls: ['./organization-edit.component.css']
})
export class OrganizationEditComponent extends BaseComponent implements OnInit {

  private model: OrganizationModel;
  private isEdit: boolean;
  constructor() {
    super();
    this.model = new OrganizationModel();
    var id = this._activatedRoute.snapshot.paramMap.get('id');
    if (id) this.isEdit = true;
  }

  ngOnInit() {
    if (this.isEdit) {
      var id = this._activatedRoute.snapshot.paramMap.get('id');
      var url = this._configService.getEndpoint(`organization/${id}`);
      this._clientService.get<OrganizationModel>(url).subscribe(data => this.model = data);
    } else {
      this.model = new OrganizationModel();
    }
  }

}
