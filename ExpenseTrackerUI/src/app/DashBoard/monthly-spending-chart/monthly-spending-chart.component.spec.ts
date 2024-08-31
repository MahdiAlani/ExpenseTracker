import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonthlySpendingChartComponent } from './monthly-spending-chart.component';

describe('MonthlySpendingChartComponent', () => {
  let component: MonthlySpendingChartComponent;
  let fixture: ComponentFixture<MonthlySpendingChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MonthlySpendingChartComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MonthlySpendingChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
