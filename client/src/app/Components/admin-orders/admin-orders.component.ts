import { Component, OnInit } from '@angular/core';
import { Chart } from 'chart.js';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AdminService } from '../../Services/admin.service';

@Component({
  selector: 'app-admin-orders',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './admin-orders.component.html',
  styleUrl: './admin-orders.component.css'
})
export class AdminOrdersComponent implements OnInit {
  orders: any[] = [];
  paginatedOrders: any[] = [];
  currentPage = 1;
  pageSize = 10;
  totalPages: number = 0;
  month: number = 0;
  year: number = 0;
  email: string = '';
  public chart:any;

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.getOrders();
    this.createChart();
  }


  createChart(){

    this.chart = new Chart("OrderAnalytics", {
      // type: 'line',
      type: 'line', 
      data: {
        labels: ['2022-05-10', '2022-05-11', '2022-05-12','2022-05-13',
                                 '2022-05-14', '2022-05-15', '2022-05-16','2022-05-17', ], 
           datasets: [
          {
            label: "Sales",
            data: ['467','576', '572', '79', '92',
                                 '574', '573', '576'],
            backgroundColor: 'blue'
          },
          {
            label: "Profit",
            data: ['542', '542', '536', '327', '17',
                                     '0.00', '538', '541'],
            backgroundColor: 'limegreen'
          }  
        ]
      },
      options: {
        aspectRatio:2.5
      }
    });
  }

  getOrders(): void {
    this.adminService.getAllOrders().subscribe(response => {
      this.orders = response.$values;
      this.paginateOrders();
    });
  }

  filterOrders(): void {
    if (this.month && this.year) {
      this.adminService.filterOrders(this.month,this.year).subscribe(response => {
        this.orders = response.$values;
        this.paginateOrders();
      });
    } else {
      this.getOrders();
    }
  }

  filterOrdersByEmail(): void {
    
  }

  paginateOrders(): void {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    this.paginatedOrders = this.orders.slice(startIndex, startIndex + this.pageSize);
    this.totalPages = Math.ceil(this.orders.length / this.pageSize);
  }

  changePage(page: number): void {
    if (page > 0 && page <= this.totalPages) {
      this.currentPage = page;
      this.paginateOrders();
    }
  }

}
