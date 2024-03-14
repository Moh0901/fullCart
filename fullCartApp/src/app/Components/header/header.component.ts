import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent {

  jwt:any = localStorage.getItem("jwt");
  role:any=localStorage.getItem("role")

  constructor(private router:Router){}
  
  logout=()=>{
    localStorage.clear();
    this.router.navigate([""]);
    setTimeout(function(){window.location.reload(); }, 100);
  }
}
