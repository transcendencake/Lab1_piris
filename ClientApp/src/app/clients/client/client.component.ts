import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterParamsService } from '../../router-params.service';

@Component({
  selector: 'app-client',
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css']
})
export class ClientComponent implements OnInit {
  public lookUps?: ILookupsModel;
  public client?: IClientModel;
  public isCreateMode: boolean = false;

  constructor(private route: ActivatedRoute,
              private http: HttpClient,
              private router: Router,
              private params: RouterParamsService,
              @Inject('BASE_URL') private baseUrl: string) { }

  async ngOnInit(): Promise<void> {
    this.isCreateMode = !this.params.clientId;
    this.lookUps = await firstValueFrom(this.http.get<ILookupsModel>(this.baseUrl + 'lookups'));
    this.refreshClient();
  }

  private async refreshClient(): Promise<void> {
    const clientId = this.params.clientId;
    if (!this.isCreateMode) {
      this.client = await firstValueFrom(this.http.get<IClientModel>(this.baseUrl + 'clients/' + clientId));
    }
  }

  public async deleteClient(): Promise<void> {
    await firstValueFrom(this.http.post<void>(this.baseUrl + 'clients/' + this.params.clientId + '/delete', null));
    await this.router.navigateByUrl(`/clients`);
  }
}
