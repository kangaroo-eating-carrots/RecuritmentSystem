   M S B A M L   `   `   `  ����L  HRecuritmentSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:RecuritmentSystem20122791RecuritmentSystem20122791  5   	   -    'RecuritmentSystem20122791.JobEditWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:RecuritmentSystem20122791     5   	     � Title$  JobEditWindow��5   	   $	��600��6   $	��350��6,   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��15 �6   �� 5      $
��1.8* �6   �� 5      $��2* �6   �� 5      $��15 �6    � RowDefinitions 5   
   �� 5      $��16 �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $
��1.3* �6   �� 5      $
��0.7* �6   �� 5      $��16 �6   .+�5   
   �� $��1��6   $��JOB TYPE��6    $��Center=�63   $��1��6P   $��Center=�6]   $
��Bold"�6x   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5   
   $��1��6   $��LENGTH (Days)��6    $��Center=�68   $��2��6U   $��Center=�6b   $
��Bold"�6}   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5   
   $��1��6   $��TOTAL COST ($)��6    $��Center=�69   $��3��6V   $��Center=�6c   $
��Bold"�6~   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5    
   $��1��6   $��
START DATE��6    $��Center=�65   $��4��6R   $��Center=�6_   $
��Bold"�6z   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5!   
   $��1��6   $��END DATE��6    $��Center=�63   $��5��6P   $��Center=�6]   $
��Bold"�6x   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5"   
   $��1��6   $��STATUS��6    $��Center=�61   $��6��6N   $��Center=�6[   $
��Bold"�6v   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5#   
   $��1��6   $��DESCRIPTION��6    $��Center=�66   $��7��6S   $��Center=�6`   $
��Bold"�6{   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5$   
   -   5         ��Name$ ComboboxTypes��5$      $��2��6*   $��Right=�6:   $��1��6V   $	��140��6c   $��40��6o   $��Center=�6{   $��18(�6�   $��Center=�6�   $��0,0,10,0q�6�   .@�5%      �� -   5        $��JobDrill��5%      $��Drill��6+   $��15(�6;   �� 5&      -   5        $��JobExcavator��5&      $��	Excavator��6/   $��15(�6C   �� 5'      -   5        $��JobTruck��5'      $��Truck��6+   $��15(�6;   �� 5(      -   5        $��
JobCrusher��5(      $��Crusher��6-   $��15(�6?   �� 5)      -   5        $��JobReclaimer��5)      $��	Reclaimer��6/   $��15(�6C   �� 5*      -   5        $��JobTrain��5*      $��Train��6+   $��15(�6;   5+      �� 5,   
   -   5         ��Name$ TextboxLength��5,      $��2��6)   $��Right=�69   $��0,0,10,0q�6U   $��2��6g    �� TextWrapping$
 Wrap=�6t   $�� ��6�   $��Center=�6�   $	��140��6�   $��40��6�   $��18(�6�   $
��Bold"�6�   $��Center=�6�   $��Right=�6  �� 5-   
   -	   5        $ TextboxCost��5-      $��2��6(   $��Right=�68   $��0,0,10,0q�6T   $��3��6f   $
 Wrap=�6s   $�� ��6�   $��Center=�6�   $	��140��6�   $��40��6�   $��18(�6�   $
��Bold"�6�   $��Center=�6�   $��Right=�6  �� 5.   
   -
   5        $ TextboxStart��5.      $��2��6)   $��Right=�69   $��0,0,10,0q�6U   $��4��6g   $
 Wrap=�6t   $�� ��6�   $��Center=�6�   $	��140��6�   $��40��6�   $��18(�6�   $
��Bold"�6�   $��Center=�6�   $��Right=�6  �� 5/   
   -   5        $ 
TextboxEnd��5/      $��2��6'   $��Right=�67   $��0,0,10,0q�6S   $��5��6e   $
 Wrap=�6r   $�� ��6�   $��Center=�6�   $	��140��6�   $��40��6�   $��18(�6�   $
��Bold"�6�   $��Center=�6�   $��Right=�6  �� 50   
   -   5        $ ComboboxStatus��50      $��2��6+   $��Right=�6;   $��6��6W   $	��140��6d   $��40��6p   $��Center=�6|   $��18(�6�   $��Center=�6�   $��0,0,10,0q�6�   .@�51      �� -   5        $��
CurrentJob��51      $��Current��6-   $��15(�6?   �� 52      -   5        $��CompleteJob��52      $��	Completed��6.   $��15(�6B   �� 53      -   5        $��CancelledJob��53      $��	Cancelled��6/   $��15(�6C   54      �� 55   
   -   5        $ TextboxDescription��55      $��2��6.   $��Right=�6>   $��0,0,10,0q�6Z   $��7��6l   $��2��6y   $
 Wrap=�6�   $+��%Please describe the job details here.��6�   $��Center=�6�   $	��140��6�   $	��130��6�   $��13(�6   �� 56   
   -   5         ��Name$ ButtonConfirm��56      $��CONFIRM��6(   $��2��6:   $��0,0,10,0q�6J   $��9��6\   $��38��6i   $	��140��6u   $��Center=�6�   $��Right=�6�   $��Black"�6�   $��18(�6�   ���	#FFB4F6A06�   57      58      