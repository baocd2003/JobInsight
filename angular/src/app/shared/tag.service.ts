import { Injectable } from '@angular/core';

export interface TagSelections {
  career?: string[];
  wageRange?: string[];
  skills?: string[];
  interests?: string[];
}

@Injectable({ providedIn: 'root' })
export class TagService {
  private selections: TagSelections = {};

  getSelections(): TagSelections {
    return this.selections;
  }

  saveSelections(s: TagSelections) {
    this.selections = { ...this.selections, ...s };
    console.log('Saved tag selections', this.selections);
  }

  clear() {
    this.selections = {};
  }
}
