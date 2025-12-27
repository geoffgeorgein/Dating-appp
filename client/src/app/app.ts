import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  private http =inject(HttpClient);
  protected readonly title = 'Dating app';

  protected members=signal<any>([])

  ngOnInit(): void {
    this.http.get('http://localhost:5270/api/members').subscribe({

      next: Response=>this.members.set(Response),
      error : error=>console.log(error),
      complete:()=>console.log('Completed the http request')

      
    })
    // throw new Error('Method not Implemented')
  }
}
