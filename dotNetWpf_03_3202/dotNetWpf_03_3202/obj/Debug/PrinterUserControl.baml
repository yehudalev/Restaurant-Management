   M S B A M L   `   `   `  ����L  HdotNetWpf_03_3202, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:dotNetWpf_03_3202dotNetWpf_03_3202  5      *    $dotNetWpf_03_3202.PrinterUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:dotNetWpf_03_3202     5      
���   �$��1q�5   "   .��5	      �   � RowDefinitions  5
   
   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   � $��2��6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��1* �6     5      �� 5      $
��auto �6    �� 5      $
��auto �6    .+�5      �� -   5         ��Name$ InkLabel��5      $	��Ink��6&   �� 5      -   5        $ PageLabe��5      $��1��6&   $��Pages��63   � 5      -   5        $��InkCountProgressBar��5   *   $��1��6    � Value$ 23Q�6E   �� 5      -   5         ��Name$ PageCountSlider��5      $��1��6.   $��1��6?   $	 400Q�6L    � Maximum$	 400Q�6X   5      �� 5   
   $��0��6   $��Images/printer.jpg��6   �� 5   
   -   5        $ PrinterNameLabel��5      $��1��6*   $��Printer Name��67   $��Center=�6N   
����iA�$��16(�6�   5      5       