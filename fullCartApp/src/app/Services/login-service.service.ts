import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginServiceService {

  loginAPIUrl = "https://localhost:7201/api/Auth";
  constructor(private http:HttpClient) { }

  login(user:any){
    return this.http.post(this.loginAPIUrl,user,{responseType:'text'});
  }

  isLoggedin():boolean{
    return localStorage.getItem("jwt")?true:false;
  }
}
