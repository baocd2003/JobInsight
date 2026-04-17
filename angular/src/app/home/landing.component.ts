import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '@abp/ng.core';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss'],
})
export class LandingComponent implements AfterViewInit {
  @ViewChild('fileInput') fileInputRef!: ElementRef<HTMLInputElement>;

  // CV upload state
  selectedFile: File | null = null;
  targetJobTitle = '';
  isAnalysing = false;
  isDragOver = false;
  analysisResult: any = null;
  analysisError: string | null = null;

  get isLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }

  get fileName(): string {
    return this.selectedFile?.name ?? '';
  }

  get scoreColor(): string {
    const score = this.analysisResult?.marketMatchScore ?? 0;
    if (score >= 70) return '#4ade80';
    if (score >= 45) return '#facc15';
    return '#f87171';
  }

  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) {}

  ngAfterViewInit(): void {
    this.initScrollAnimations();
  }

  triggerFileInput(): void {
    this.fileInputRef?.nativeElement.click();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) {
      this.selectedFile = input.files[0];
      this.analysisResult = null;
      this.analysisError = null;
    }
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault();
    this.isDragOver = true;
  }

  onDragLeave(): void {
    this.isDragOver = false;
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    this.isDragOver = false;
    const file = event.dataTransfer?.files[0];
    if (file) {
      this.selectedFile = file;
      this.analysisResult = null;
      this.analysisError = null;
    }
  }

  analyseCV(): void {
    if (!this.isLoggedIn) {
      this.router.navigate(['/auth/login']);
      return;
    }
    if (!this.selectedFile || !this.targetJobTitle.trim()) return;

    this.isAnalysing = true;
    this.analysisError = null;
    this.analysisResult = null;

    const formData = new FormData();
    formData.append('file', this.selectedFile);
    formData.append('targetJobTitle', this.targetJobTitle.trim());

    this.http.post<any>('/api/app/cv/analyse', formData).subscribe({
      next: (result) => {
        this.analysisResult = result;
        this.isAnalysing = false;
      },
      error: (err) => {
        this.analysisError = err?.error?.error?.message ?? 'Analysis failed. Please try again.';
        this.isAnalysing = false;
      },
    });
  }

  resetUpload(): void {
    this.selectedFile = null;
    this.analysisResult = null;
    this.analysisError = null;
    this.targetJobTitle = '';
    if (this.fileInputRef) {
      this.fileInputRef.nativeElement.value = '';
    }
  }

  private initScrollAnimations(): void {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach((entry) => {
          if (entry.isIntersecting) {
            entry.target.classList.add('visible');
            observer.unobserve(entry.target);
          }
        });
      },
      { threshold: 0.08 }
    );
    document.querySelectorAll('.fade-in').forEach((el) => observer.observe(el));
  }
}
