import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {firstValueFrom} from "rxjs";
import {RouterParamsService} from "../../../router-params.service";

@Component({
  selector: 'app-deposits',
  templateUrl: './deposits.component.html',
  styleUrls: ['./deposits.component.css']
})
export class DepositsComponent implements OnInit {
  public deposits: ISelectedItemModel[] = [];

  constructor(private http: HttpClient,
              private router: Router,
              private params: RouterParamsService,
              @Inject('BASE_URL') private baseUrl: string) { }

  async ngOnInit(): Promise<void> {
    this.deposits = await firstValueFrom(this.http.get<ISelectedItemModel[]>(this.baseUrl + 'clients/' + this.params.clientId + '/deposit'));
  }

  async goToDeposit(id: number): Promise<void> {
    await this.router.navigateByUrl(`/clients/${this.params.clientId}/deposits/${id}`);
  }
}
