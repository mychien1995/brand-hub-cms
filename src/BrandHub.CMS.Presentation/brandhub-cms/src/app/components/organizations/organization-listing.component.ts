import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../../services/shared/config.service';
import { ClientService } from '../../services/shared/client.service';
import { SearchResult } from '../../models/searchresult.model';
import { OrganizationModel } from '../../models/organizations/organization.model';
import { BaseComponent } from '../base.component';

@Component({
  selector: 'app-organization-listing',
  templateUrl: './organization-listing.component.html',
  styleUrls: ['./organization-listing.component.css']
})
export class OrganizationListingComponent extends BaseComponent implements OnInit {

  searchResult: SearchResult<OrganizationModel> ;

  constructor(_configService: ConfigService, _clientService: ClientService) {
    super(_configService, _clientService);

  }

  ngOnInit() {
    var url = this._configService.getEndpoint('organization/search');
    this._clientService.get<SearchResult<OrganizationModel>>(url).subscribe(data => {
      this.searchResult = data;
    });
  }

}
