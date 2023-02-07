import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.scss']
})
export class ClientsComponent implements OnInit {
  public clients: IClientModel[] = [];

  constructor(private http: HttpClient,
              private router: Router,
              @Inject('BASE_URL') private baseUrl: string) { }

  async ngOnInit(): Promise<void> {
    this.clients = await firstValueFrom(this.http.get<IClientModel[]>(this.baseUrl + 'clients'));
  }

  async goToClient(id: number): Promise<void> {
    await this.router.navigateByUrl(`/clients/${id}`);
  }
}
