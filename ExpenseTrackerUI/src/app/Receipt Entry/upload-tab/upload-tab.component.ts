import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-upload-tab',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './upload-tab.component.html',
  styleUrl: './upload-tab.component.css'
})
export class UploadTabComponent {

  protected files: File[] = [];

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onDrop(event: DragEvent) {
    
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];

    if (file) {
      // Check if the file is already in the array based on name and size
      const isDuplicate = this.files.some(existingFile => 
        existingFile.name === file.name && existingFile.size === file.size
      );

      // Only add the file if it's not a duplicate
      if (!isDuplicate) {
        this.files.push(file);
      }

      event.target.value = '';
      console.log(this.files);
    }
  }

  removeFile(file: File) {
    this.files = this.files.filter(item => item !== file);
    console.log(file + " removed");
    console.log(this.files)
  }
}
