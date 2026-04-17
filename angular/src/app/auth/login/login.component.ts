import { Component, AfterViewInit, ElementRef, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { ConfigStateService } from '@abp/ng.core';
import { lastValueFrom } from 'rxjs';
import { environment } from '../../../environments/environment';

declare const google: any;

interface GoogleLoginResult {
  accessToken: string;
  tokenType: string;
  expiresIn: number;
  refreshToken: string | null;
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements AfterViewInit {
  @ViewChild('googleBtn') googleBtn!: ElementRef;

  isLoading = false;
  error = '';

  private readonly apiUrl = environment.apis.default.url;

  constructor(
    private http: HttpClient,
    private router: Router,
    private configState: ConfigStateService
  ) {}

  ngAfterViewInit(): void {
    google.accounts.id.initialize({
      client_id: environment.googleClientId,
      callback: this.handleCredentialResponse.bind(this),
    });

    google.accounts.id.renderButton(this.googleBtn.nativeElement, {
      theme: 'outline',
      size: 'large',
      width: 300,
      text: 'signin_with',
    });
  }

  async handleCredentialResponse(response: { credential: string }): Promise<void> {
    this.isLoading = true;
    this.error = '';

    try {
      const result = await lastValueFrom(
        this.http.post<GoogleLoginResult>(`${this.apiUrl}/api/app/google-auth/login`, {
          idToken: response.credential,
        })
      );

      localStorage.setItem('access_token', result.accessToken);
      localStorage.setItem('token_type', result.tokenType);
      localStorage.setItem('access_token_stored_at', Date.now().toString());
      localStorage.setItem('expires_at', (Date.now() + result.expiresIn * 1000).toString());
      if (result.refreshToken) {
        localStorage.setItem('refresh_token', result.refreshToken);
      }

      await lastValueFrom(this.configState.refreshAppState());
      this.router.navigate(['/']);
    } catch {
      this.error = 'Google login failed. Please try again.';
    } finally {
      this.isLoading = false;
    }
  }
}
