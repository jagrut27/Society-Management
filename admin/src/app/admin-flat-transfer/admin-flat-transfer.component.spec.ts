import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminFlatTransferComponent } from './admin-flat-transfer.component';

describe('AdminFlatTransferComponent', () => {
  let component: AdminFlatTransferComponent;
  let fixture: ComponentFixture<AdminFlatTransferComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdminFlatTransferComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminFlatTransferComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
