import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthService } from '@abp/ng.core';

type PageState = 'upload' | 'loading' | 'result';

const MOCK_RESULT = {
  targetJobTitle: 'Backend Developer',
  referencedJobCount: 47,
  marketMatchScore: 72,
  strengths: [
    'Strong proficiency in C# and .NET ecosystem',
    'Solid understanding of RESTful API design patterns',
    'Experience with relational databases (PostgreSQL, SQL Server)',
    'Familiar with containerisation using Docker',
  ],
  weaknesses: [
    'Limited exposure to microservices architecture at scale',
    'No demonstrated experience with message brokers (RabbitMQ, Kafka)',
    'Cloud deployment experience not clearly evidenced',
  ],
  missingSkills: [
    { skillName: 'Kubernetes', priority: 'High', frequencyPercent: 68 },
    { skillName: 'RabbitMQ', priority: 'High', frequencyPercent: 54 },
    { skillName: 'Redis', priority: 'High', frequencyPercent: 61 },
    { skillName: 'gRPC', priority: 'Medium', frequencyPercent: 38 },
    { skillName: 'AWS / Azure', priority: 'Medium', frequencyPercent: 72 },
    { skillName: 'Terraform', priority: 'Medium', frequencyPercent: 29 },
    { skillName: 'GraphQL', priority: 'Low', frequencyPercent: 21 },
    { skillName: 'Elasticsearch', priority: 'Low', frequencyPercent: 18 },
  ],
  extractedSkills: ['C#', '.NET 8', 'PostgreSQL', 'Docker', 'REST API', 'EF Core', 'Git', 'SQL Server'],
};

const LOADING_STEPS = [
  'Reading your CV…',
  'Extracting skills and experience…',
  'Scanning 47 job postings…',
  'Calculating your market match…',
  'Almost there…',
];

@Component({
  selector: 'app-cv-analyse',
  templateUrl: './cv-analyse.component.html',
  styleUrls: ['./cv-analyse.component.scss'],
})
export class CvAnalyseComponent implements AfterViewInit {
  @ViewChild('fileInput') fileInputRef!: ElementRef<HTMLInputElement>;

  state: PageState = 'upload';
  selectedFile: File | null = null;
  targetJobTitle = '';
  isDragOver = false;
  analysisResult: any = null;
  analysisError: string | null = null;

  loadingStep = 0;
  loadingText = LOADING_STEPS[0];
  private loadingInterval: any;

  get isLoggedIn(): boolean { return this.authService.isAuthenticated; }
  get fileName(): string { return this.selectedFile?.name ?? ''; }
  get fileSize(): string {
    const bytes = this.selectedFile?.size ?? 0;
    return bytes > 1024 * 1024
      ? (bytes / 1024 / 1024).toFixed(1) + ' MB'
      : Math.round(bytes / 1024) + ' KB';
  }

  get scoreColor(): string {
    const score = this.analysisResult?.marketMatchScore ?? 0;
    if (score >= 70) return '#4ade80';
    if (score >= 45) return '#facc15';
    return '#f87171';
  }

  get scoreLabel(): string {
    const score = this.analysisResult?.marketMatchScore ?? 0;
    if (score >= 70) return 'Strong match';
    if (score >= 45) return 'Moderate match';
    return 'Needs work';
  }

  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService,
  ) {}

  ngAfterViewInit(): void {}

  triggerFileInput(): void { this.fileInputRef?.nativeElement.click(); }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files?.length) this.selectedFile = input.files[0];
  }

  onDragOver(event: DragEvent): void { event.preventDefault(); this.isDragOver = true; }
  onDragLeave(): void { this.isDragOver = false; }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    this.isDragOver = false;
    const file = event.dataTransfer?.files[0];
    if (file) this.selectedFile = file;
  }

  analyseCV(): void {
    if (!this.selectedFile || !this.targetJobTitle.trim()) return;
    this.startLoading();
  }

  private startLoading(): void {
    this.state = 'loading';
    this.loadingStep = 0;
    this.loadingText = LOADING_STEPS[0];

    this.loadingInterval = setInterval(() => {
      this.loadingStep = (this.loadingStep + 1) % LOADING_STEPS.length;
      this.loadingText = LOADING_STEPS[this.loadingStep];
    }, 900);

    // Simulate API delay then show mock result
    setTimeout(() => {
      clearInterval(this.loadingInterval);
      this.analysisResult = { ...MOCK_RESULT, targetJobTitle: this.targetJobTitle.trim() };
      this.state = 'result';
    }, 4500);
  }

  resetUpload(): void {
    this.state = 'upload';
    this.selectedFile = null;
    this.analysisResult = null;
    this.analysisError = null;
    this.targetJobTitle = '';
    clearInterval(this.loadingInterval);
    if (this.fileInputRef) this.fileInputRef.nativeElement.value = '';
  }
}
