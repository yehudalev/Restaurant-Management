   M S B A M L   `   `   `  ����B  >PLForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:PLFormsPLForms  5   	       PLForms.statistics   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:PLForms     5   	     � Title$  
statistics��5   	   $
��auto��6   $
��auto��6*    � 	Resources 5	         
myTemplate � RowDefinitions �� Orientation&   System.Windows.Controls.DataGrid��%�  &	        �� 5
   
   .R�5      �  5      �� 5      $
��auto �6$   �� 5      $
��auto �6$   .+�5      �� $ 
Horizontal=�6   .+�5      �� $��Center=�65   $��20(�6R   ��6    �� *Key+6R   �� 5      .��6   
Group Size6(   �� 5      $��Center=�67   $��20(�6R   ��6    �� *Count+6R   5        5      $��1��6   ��6(   �� 5      5      5      .��5      �  5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��* �6    � ColumnDefinitions 5!   
   �� 5"      $��* �6   �� 5#      $��* �6   .+�5%   
   �� -   5         ��Name$ ordersComboBox��5%      $��1��6+   .@�5&      �� $
��City��6   �� 5'      $��Profit Range��6   �� 5(      $��Month��6   5)      �� 5*   
   -   5        $ disheshComboBox��5*      $��1��6,   $��1��69   .@�5+      �� $��Kashrut��6   �� 5,      $
��Size��6   �� 5-      $��Item 3��6   5.      �� 5/   
   $��Orders Group by:��6   $��15(�6+   
����� �69   �� 50   
   $��Dishes Group by:��6   $��15(�6+   
����� �$��1��6K   �� 51   
   -   5         ��Name$ orderListBox��51      #��[  6(   $��2��6S   �� 52   
   -   5        $ dishListBox��52      #��[  6'   $��2��6R   $��2��6_   54      55      