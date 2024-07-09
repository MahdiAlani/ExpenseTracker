import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiptFormFieldComponent } from './receipt-form-field.component';

describe('ReceiptFormFieldComponent', () => {
  let component: ReceiptFormFieldComponent;
  let fixture: ComponentFixture<ReceiptFormFieldComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptFormFieldComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceiptFormFieldComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
