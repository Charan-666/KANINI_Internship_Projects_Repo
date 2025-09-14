import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodCart } from './food-cart';

describe('FoodCart', () => {
  let component: FoodCart;
  let fixture: ComponentFixture<FoodCart>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FoodCart]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodCart);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
