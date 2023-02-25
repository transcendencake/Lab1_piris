import { Component, OnInit } from '@angular/core';
import {RouterParamsService} from "../../router-params.service";

@Component({
  selector: 'app-client-general',
  templateUrl: './client-general.component.html',
  styleUrls: ['./client-general.component.css']
})
export class ClientGeneralComponent implements OnInit {
  public generalInfoLink: string = '';
  public accountsLink: string = '';
  public depositsLink: string = '';
  public creditsLink: string = '';
  public isCreateMode: boolean = true;

  constructor(private params: RouterParamsService) { }

  ngOnInit(): void {
    this.isCreateMode = !this.params.clientId;
    this.generalInfoLink = `/clients/${this.params.clientId || 0}/general`
    this.depositsLink = `/clients/${this.params.clientId}/deposits`
    this.creditsLink = `/clients/${this.params.clientId}/credits`
    this.accountsLink = `/clients/${this.params.clientId}/accounts`
  }
}
