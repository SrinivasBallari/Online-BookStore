import { Component, OnInit, AfterViewInit } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../Services/admin.service';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-admin-orders',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    CanvasJSAngularChartsModule,
  ],
  templateUrl: './admin-orders.component.html',
  styleUrl: './admin-orders.component.css',
})
export class AdminOrdersComponent implements OnInit, AfterViewInit {
  orders: any[] = [];
  paginatedOrders: any[] = [];
  currentPage = 1;
  pageSize = 10;
  totalPages: number = 0;
  month: number = 0;
  year: number = 2024;
  email: string = '';
  public chartOptions: any;
  monthlyOrderCounts: any[] = [];

  constructor(
    private adminService: AdminService, 
  ) {}

  ngOnInit(): void {
    this.getOrders(); 
  }

  ngAfterViewInit(): void {
    this.createEmptyChart();
  }

  createEmptyChart(): void {
    this.chartOptions = {
      title: {
        text: "Monthly Orders of 2024"
      },
      animationEnabled: true,
      data: [{
        type: "column",
        dataPoints: []
      }]
    };
  }

  resetFilters(): void {
    this.month = 0;
    this.getOrders();
  }

  getOrders(): void {
    this.adminService.getAllOrders().subscribe((response) => {
      this.orders = response.$values;
      this.paginateOrders();
      this.calculateMonthlyOrderCounts();
      this.updateChart();
    });
  }

  calculateMonthlyOrderCounts(): void {
    const monthlyCounts = Array(12).fill(0); 

    this.orders.forEach(order => {
      const orderDate = new Date(order.orderDate);
      if (orderDate.getFullYear() === this.year) {
        const month = orderDate.getMonth(); 
        monthlyCounts[month]++;
      }
    });

    this.monthlyOrderCounts = monthlyCounts.map((count, index) => ({
      x: index + 1,
      y: count,
    }));
  }

  updateChart(): void {
    const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
  
    // Map the monthly counts with month names for better readability on the x-axis
    const labeledMonthlyOrderCounts = this.monthlyOrderCounts.map((data, index) => ({
      x: index + 1,
      label: monthNames[index],  
      y: data.y 
    }));
  
    this.chartOptions = {
      title: {
        text: "Monthly Orders of 2024"
      },
      axisX: {
        title: "Months", 
        interval: 1,
        labelFormatter: function(e: any) {
          return monthNames[e.value - 1]; 
        }
      },
      axisY: {
        title: "Order Count", 
        includeZero: true
      },
      animationEnabled: true,
      data: [{
        type: "column",
        dataPoints: labeledMonthlyOrderCounts 
      }]
    };
  }

  filterOrders(): void {
    if (this.month && this.year) {
      this.adminService
        .filterOrders(this.month, this.year)
        .subscribe((response) => {
          this.orders = response.$values;
          this.paginateOrders();
          this.calculateMonthlyOrderCounts();
          this.updateChart();
        });
    } else {
      this.getOrders();
    }
  }

  paginateOrders(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    this.paginatedOrders = this.orders.slice(
      startIndex,
      startIndex + this.pageSize
    );
    this.totalPages = Math.ceil(this.orders.length / this.pageSize);
  }

  changePage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.paginateOrders();
    }
  }
}
