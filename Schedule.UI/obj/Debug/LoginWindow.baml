   M S B A M L   `   `   `  ����F  BSchedule.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Schedule.UISchedule.UI  5   	       Schedule.UI.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Schedule.UI     5   	     � Title$  LoginWindow��5   	   $	��500��6   $	��300��6*   .��5	      � $��5q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��15(�6   $��	0 150 0 0q�6"   $��Center=�65   Enter your data to sign in6n   � 5   
   $��1��6    5      �� 5      �� 5      .+�5      �� -   5        $��textBoxEmail��5      $	��200��6M   $��20(�6Y   $��Center=�6g   $��5q�6�   6�   �� 5      -   5        $��fakeTextBoxEmail��5      ���#d8d8d8$	��200��6h   $��20(�6t   $��Center=�6�   $��5q�6�   .��6�   Email6�   �� 5      -   5        $��wrongEmailMessage��5      $y�	Collapsed=�61   $��1��6H   $��Center=�6U   $	��200��6r   
���  ��" There is no user with such email6�   �� 5      -   5        $��emptyEmailMessage��5      $y�	Collapsed=�61   $��1��6H   $��Center=�6U   $	��200��6r   
���  ��Email must be not empty6�   5      � 5   
   $��2��6    5      �� 5      �� 5      .+�5!      G� -   5        $��passwordBoxPassword��5!      $��20(�65   $��Center=�6C   $	��200��6^   $��5q�6j   6�   �� 5"      -   5        $��textBoxPassword��5"      ���#d8d8d8$	��200��6f   $��20(�6r   $��Center=�6�   $��5q�6�   .��6�   
Password6�   �� 5#      -   5        $��wrongPasswordMessage��5#      $y�	Collapsed=�64   $��1��6K   $��Center=�6X   $	��200��6u   
���  ��Invalid password6�   �� 5$      -   5        $��emptyPasswordMessage��5$      $y�	Collapsed=�64   $��1��6K   $��Center=�6X   $	��200��6u   
���  ��" Password field must be not empty6�   5%      �� 5&   
   -	   5        $��30��5&      $��3��6   $��5q�6*   $	��200��65   $��18(�6]   .��6k   	Sign In6t   �� 5'   
   $��Bottom=�6   $��4��60   .+�5(      �� $	��200��6   $��Center=�6$   $��15(�6A   Do not have an account yet?6l   �� 5)      -
   5        $��30��5)      $��4��6!   $��5q�6.   $	��200��69   $��18(�6c   .��6q   
Register6{   5*      5+      5,      