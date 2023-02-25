import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {RouterParamsService} from "../../../router-params.service";
import {firstValueFrom} from "rxjs";

@Component({
  selector: 'app-credits',
  templateUrl: './credits.component.html',
  styleUrls: ['./credits.component.css']
})
export class CreditsComponent implements OnInit {
  public credits: ISelectedItemModel[] = [];

  constructor(private http: HttpClient,
              private router: Router,
              private params: RouterParamsService,
              @Inject('BASE_URL') private baseUrl: string) { }

  async ngOnInit(): Promise<void> {
    this.credits = await firstValueFrom(this.http.get<ISelectedItemModel[]>(this.baseUrl + 'clients/' + this.params.clientId + '/credit'));
  }

  async goToCredit(id: number): Promise<void> {
    await this.router.navigateByUrl(`/clients/${this.params.clientId}/credits/${id}`);
  }
}
