import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadTabComponent } from './upload-tab.component';

describe('UploadTabComponent', () => {
  let component: UploadTabComponent;
  let fixture: ComponentFixture<UploadTabComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UploadTabComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UploadTabComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
