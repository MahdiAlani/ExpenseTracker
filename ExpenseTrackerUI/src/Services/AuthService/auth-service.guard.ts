import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from './Auth.service';
import { map, catchError, of } from 'rxjs';

export const authServiceGuard: CanActivateFn = () => {
    const authService = inject(AuthService);
    const router = inject(Router);

    return authService.isAuthenticated().pipe(
      map(isAuth => {
        if (isAuth) {
          return true;
        } else {
          router.navigate(['SignIn']);
          return false;
        }
      }),
      catchError(() => {
        router.navigate(['SignIn']);
        return of(false);
      })
    );
  };
  