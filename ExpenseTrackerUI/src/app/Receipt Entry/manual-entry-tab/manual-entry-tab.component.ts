import { Component } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-manual-entry-tab',
  templateUrl: './manual-entry-tab.component.html',
  styleUrls: ['./manual-entry-tab.component.css'],
})
export class ManualEntryTabComponent {

  ngAfterViewInit() {
    this.validateForm();
  }

  validateForm() {
    var forms = document.querySelectorAll('.needs-validation')

  // Loop over them and prevent submission
  Array.prototype.slice.call(forms)
    .forEach(function (form) {
      form.addEventListener('submit', function (event: any) {
        if (!form.checkValidity()) {
          event.preventDefault()
          event.stopPropagation()
        }

        form.classList.add('was-validated')
      }, false)
    })
  }
}
