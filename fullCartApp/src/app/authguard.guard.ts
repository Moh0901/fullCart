import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, RouterStateSnapshot, UrlTree } from '@angular/router';
import { LoginServiceService } from './Services/login-service.service';
import { Observable } from 'rxjs';


export class AuthGuard implements CanActivate {
  constructor(private loginService:LoginServiceService){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      
      const token = localStorage.getItem("jwt");
      return this.loginService.isLoggedin();
  }
  
}
