import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TagService } from '../../shared/tag.service';

interface Option {
  id: string;
  label: string;
}

@Component({
  standalone: true,
  selector: 'app-tag-selection',
  imports: [CommonModule],
  templateUrl: './tag-selection.component.html',
  styleUrls: ['./tag-selection.component.scss'],
})
export class TagSelectionComponent {
  categories = [
    {
      key: 'career',
      title: 'What is your career?',
      multi: false,
      options: [
        { id: 'se', label: 'Software Engineer' },
        { id: 'pm', label: 'Product Manager' },
        { id: 'marketing', label: 'Marketing' },
        { id: 'design', label: 'Design' },
        { id: 'data', label: 'Data Scientist' },
      ] as Option[],
    },
    {
      key: 'wageRange',
      title: 'Wage range',
      multi: false,
      options: [
        { id: '0-40', label: '$0 - $40k' },
        { id: '40-80', label: '$40k - $80k' },
        { id: '80-120', label: '$80k - $120k' },
        { id: '120+', label: '$120k+' },
      ] as Option[],
    },
    {
      key: 'skills',
      title: 'Which skills do you want to improve?',
      multi: true,
      options: [
        { id: 'js', label: 'JavaScript' },
        { id: 'py', label: 'Python' },
        { id: 'sql', label: 'SQL' },
        { id: 'cloud', label: 'Cloud' },
        { id: 'ml', label: 'Machine Learning' },
      ] as Option[],
    },
    {
      key: 'interests',
      title: 'Interests',
      multi: true,
      options: [
        { id: 'management', label: 'Management' },
        { id: 'freelance', label: 'Freelance' },
        { id: 'startup', label: 'Startup' },
        { id: 'research', label: 'Research' },
      ] as Option[],
    },
  ];

  selections: Record<string, string[]> = {};

  constructor(private tagService: TagService, public router: Router) {
    // initialize empty arrays
    this.categories.forEach((c) => (this.selections[c.key] = []));
  }

  toggle(categoryKey: string, opt: Option, multi: boolean) {
    const arr = this.selections[categoryKey] || [];
    const idx = arr.indexOf(opt.id);
    if (idx > -1) {
      arr.splice(idx, 1);
    } else {
      if (!multi) {
        // single select: replace
        this.selections[categoryKey] = [opt.id];
        return;
      }
      arr.push(opt.id);
    }
    this.selections[categoryKey] = arr;
  }

  isSelected(categoryKey: string, id: string) {
    return (this.selections[categoryKey] || []).indexOf(id) > -1;
  }

  save() {
    this.tagService.saveSelections({
      career: this.selections['career'],
      wageRange: this.selections['wageRange'],
      skills: this.selections['skills'],
      interests: this.selections['interests'],
    });
    // optionally go to dashboard
    this.router.navigate(['/dashboard']);
  }
}
