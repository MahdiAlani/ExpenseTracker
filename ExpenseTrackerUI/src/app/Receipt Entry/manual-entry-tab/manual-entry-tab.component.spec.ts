import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManualEntryTabComponent } from './manual-entry-tab.component';

describe('ManualEntryTabComponent', () => {
  let component: ManualEntryTabComponent;
  let fixture: ComponentFixture<ManualEntryTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManualEntryTabComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManualEntryTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
