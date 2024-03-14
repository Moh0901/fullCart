import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';
import { LoginServiceService } from 'src/app/Services/login-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  role:any=localStorage.getItem("role")
  token:any=localStorage.getItem("jwt")
  jwt:any = localStorage.getItem("jwt");
  currentUser: BehaviorSubject<any> = new BehaviorSubject(null);

  constructor(private loginService: LoginServiceService, private router:Router){}

  onLoginForm = new FormGroup({
    username: new FormControl("",[Validators.required]),
    password: new FormControl("",[Validators.required])
  });

  onLogin()
  {
    this.loginService.login({
      username: this.onLoginForm.value.username,
      password: this.onLoginForm.value.password
    }).subscribe({
      next: (response:any)=> { console.log(response);
        localStorage.setItem('jwt',response);
        console.log("Login Successfully!"); 
        this.router.navigate(['']);
        this.onLoginForm.reset();
        this.loadCurrentUser();
        window.location.reload();
      }
    })
  }

  jwtHelperService = new JwtHelperService();
  loadCurrentUser(){
    const token:any = localStorage.getItem("jwt");
    const userinfo = this.jwtHelperService.decodeToken(token);
     const data = userinfo?{
      username:userinfo.Name,
      userrole:userinfo.Role
     }:null;
     localStorage.setItem("role",userinfo.role);
     this.currentUser.next(data);
  }
}
