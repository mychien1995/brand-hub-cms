import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../../services/shared/config.service';
import { ClientService } from '../../services/shared/client.service';
import { SearchResult } from '../../models/searchresult.model';
import { OrganizationModel } from '../../models/organizations/organization.model';

@Component({
  selector: 'app-organization-listing',
  templateUrl: './organization-listing.component.html',
  styleUrls: ['./organization-listing.component.css']
})
export class OrganizationListingComponent implements OnInit {

  constructor(private _configService: ConfigService, private _clientService: ClientService) {}

  ngOnInit() {
    var organizationList: SearchResult <OrganizationModel>;
    organizationList = new SearchResult <OrganizationModel> ();
    var url = this._configService.getEndpoint('organization/search');
    this._clientService.get(url).subscribe(data => {
    	console.log(data);
    });
  }

}
