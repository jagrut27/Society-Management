import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlatTransferComponent } from './flat-transfer.component';

describe('FlatTransferComponent', () => {
  let component: FlatTransferComponent;
  let fixture: ComponentFixture<FlatTransferComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FlatTransferComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FlatTransferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
