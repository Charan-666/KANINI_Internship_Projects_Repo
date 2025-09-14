import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodDetail } from './food-detail';

describe('FoodDetail', () => {
  let component: FoodDetail;
  let fixture: ComponentFixture<FoodDetail>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FoodDetail]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodDetail);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
