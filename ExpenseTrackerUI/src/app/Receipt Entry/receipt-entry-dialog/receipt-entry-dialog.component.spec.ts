import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiptEntryDialog } from './receipt-entry-dialog';

describe('ReceiptEntryDialog', () => {
  let component: ReceiptEntryDialog;
  let fixture: ComponentFixture<ReceiptEntryDialog>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReceiptEntryDialog]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ReceiptEntryDialog);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
