import {CanActivateFn, Router} from '@angular/router';
import {inject} from "@angular/core";
import { AuthService } from './services/auth.service';


export const adminGuard: CanActivateFn = (route, state) => {
  if (inject(AuthService).hasRole('Admin')) {
    
    return true;
  }
  return false;
};