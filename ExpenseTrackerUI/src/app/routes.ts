import { Routes } from "@angular/router";
import { HomeComponent } from "./Pages/home/home.component";
import { SignInPageComponent } from "./authPages/sign-in-page/sign-in-page.component";
import { SignUpPageComponent } from "./authPages/sign-up-page/sign-up-page.component";
import { authServiceGuard } from "../Services/AuthService/auth-service.guard";

const routeConfig: Routes = [
    {
        path: '',
        component: HomeComponent,
        title: "Home Page",
        canActivate: [authServiceGuard]
    },

    {
        path: 'SignIn',
        component: SignInPageComponent,
        title: "Sign In Page"
    },

    {
        path: 'SignUp',
        component: SignUpPageComponent,
        title: "Sign Up Page"
    }
];

export default routeConfig;