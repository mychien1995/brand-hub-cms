import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../base.component';
import { ConfigService } from '../../services/shared/config.service';
import { ClientService } from '../../services/shared/client.service';
import { OrganizationModel } from '../../models/organizations/organization.model';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-organization-edit',
  templateUrl: './organization-edit.component.html',
  styleUrls: ['./organization-edit.component.css']
})
export class OrganizationEditComponent extends BaseComponent implements OnInit {

  private model: OrganizationModel;
  private isEdit: boolean;
  private id: number;
  constructor() {
    super();
  }

  ngOnInit() {
    this.id = this.getRouteParam('id');
    if (this.id) {
      this.isEdit = true;
    } else this.model = new OrganizationModel();

    if (this.isEdit) {
      var id = this.id;
      var url = this._configService.getEndpoint(`organization/${id}`);
      this._clientService.get<OrganizationModel> (url).subscribe(data => this.model = data);
    }
  }

}
