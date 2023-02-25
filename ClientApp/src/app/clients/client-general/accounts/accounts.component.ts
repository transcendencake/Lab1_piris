import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {RouterParamsService} from "../../../router-params.service";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrls: ['./accounts.component.css']
})
export class AccountsComponent implements OnInit {
  public accounts: IAccountModel[] = [];

  constructor(private http: HttpClient,
              private router: Router,
              private params: RouterParamsService,
              @Inject('BASE_URL') private baseUrl: string) { }

  async ngOnInit(): Promise<void> {
    this.accounts = await firstValueFrom(this.http.get<IAccountModel[]>(this.baseUrl + 'accounts'));
  }

  async closeDay() {
    await firstValueFrom(this.http.post<void>(this.baseUrl + 'accounts/close-day', null));
    await this.ngOnInit();
  }
}
