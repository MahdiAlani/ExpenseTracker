import { Component } from '@angular/core';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-upload-tab',
  standalone: true,
  imports: [MatIcon],
  templateUrl: './upload-tab.component.html',
  styleUrl: './upload-tab.component.css'
})
export class UploadTabComponent {

  onDragOver(event: DragEvent) {
    event.preventDefault();
  }

  onDrop(event: DragEvent) {
    
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      console.log(file);
      // Handle the file (e.g., upload, display, etc.)
    }
  }
}
