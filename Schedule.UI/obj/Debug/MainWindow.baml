   M S B A M L   `   `   `  ����F  BSchedule.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Schedule.UISchedule.UI  5   	       Schedule.UI.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Schedule.UI     5   	     � Title$  
MainWindow��5   	   $	��500��6   $	��300��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      .+�5   
   �� -   5        $��30��5      $��80��6   $��10q�6(   $
��Left=�64   $��16(�5      .��6   	Account6(   �� 5   
   -   5        $��30��5      $��80��6   $��10q�6(   $��16(�64   $��Right=�6B   .��5   4   	Favorites6?   �� 5   
   -   5        $��comboBoxStations��5       �� DisplayMemberPath$ Station.Name��6+    �� 
IsReadOnly . $��50��6^   $��18(�6j   $��1��5   H   $��2��6U   $
��Auto��6i   $
��0 10q�6v   $��Center=�5       �� 
IsEditable . 5      �� 5   
   -   5        $��descriptionTextBlock��5   Q   $��2��6   $��20(�6!   $��5q�6/    �� TextAlignment$ Center=�6:   6o   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dataGridRoutes��5      $��3��6)   $��2��66      AutoGenerateColumns .     
IsReadOnly . 	   CanUserReorderColumns	 .  
   CanUserResizeColumns
 .  $
��Auto��5   N      CanUserResizeRows .  $��14(�5   -   $��0q�6;   $
��Left=�6G   |�.     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ Route��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 3* 64   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   �� *	RouteName+6_     5!      $ Destination��6%   $ 3* 6:    6E   �� *Destination.Name+6l     5"      $
 Time��6%   $ 1* 63    6>   �� *MinutesLeft+6`   5$      5%      5&      