   M S B A M L   `   `   `  ����B  >PLForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:PLFormsPLForms  5   	       PLForms.Manager   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:PLForms     5   	     � Title$  Manager��5   	   $	��300��6   $
��auto��6&    � 	Resources 5	      2   ,PLForms.ConvertFromDataGridSelectedToBoolian (  $convertFromDataGridSelectedToBoolian��%   &	          5
   
   65   5      .��5      �  5   
    �� 
TargetType �� Property �� Value��%�   '��       �� 5      " ��6   .�5      ��  � ��$ 	0,10,10,0q�6+   �� 5       � ��$ 50��6*   �� 5       � ��$ 50��6+   5      5       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��5* �6   �� 5      $	��30* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   �� 5       $��* �6   .+�5#   
   �� $��1��6   
����  �$��1q�64   $��3��6H   �� 5$   
   $��3��6   
����  �$��1q�64   $��3��6H   �� 5&   
    �� Orientation$ 
Horizontal=�6   $��0��6.   $��Center=�6;   .+�5'      �� -   5        $��Remove��5'       � DataContext$ orderDataGrid��6@   {�6\   �� 	 �� ElementName$	 orderDataGrid��
 �� Path$
 SelectedIndex� �� 	Converter# [  �� 5(      -   5        $��Update��5(      $ orderDataGrid��6A   {�6]   �� $	 orderDataGrid��$
 SelectedIndex�# [  5)      &   System.Windows.Controls.DataGrid  5+   
   -   5          Name$ orderDataGrid��5+       �� SelectedIndex$ -1��6*   $��2��6=   
������    
IsReadOnly . $��
0,10,0,-10q�6v   �� 5.   
   $ 
Horizontal=�6   $��0��6.   $��2��6;   $��Center=�6K   .+�5/      �� -   5        $	��Add��5/      6$   �� 50      -   5        $��Remove��50      {�6?   �� $	 dishDataGrid��$
 SelectedIndex�# [  �� 51      -   5        $��Update��51      {�6?   �� $	 dishDataGrid��$
 SelectedIndex�# [  52        54   
   -   5        $ dishDataGrid��54      $��2��6)   $��2��67   
������  . 55      �� 57   
   $ 
Horizontal=�6   $��0��6.   $��4��6;   $��Center=�6K   .+�58      �� -   5        $	��Add��58      6$   �� 59      -	   5        $��Remove��59      {�6A   �� $	 branchDataGrid��$
 SelectedIndex�# [  �� 5:      -
   5        $��Update��5:      {�6A   �� $	 branchDataGrid��$
 SelectedIndex�# [  5;        5=   
   -   5        $ branchDataGrid��5=      $��2��6+   $��4��68   
������  . 5>      5@      5A      