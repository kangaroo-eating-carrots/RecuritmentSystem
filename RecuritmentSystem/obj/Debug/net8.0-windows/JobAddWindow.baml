   M S B A M L   `   `   `  ����L  HRecuritmentSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:RecuritmentSystem20122791RecuritmentSystem20122791  5   	   ,    &RecuritmentSystem20122791.NewJobWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:RecuritmentSystem20122791     5   	     � Title$  NewJobWindow��5   	   $	��450��6   $	��350��6+   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��15 �6   �� 5      $
��1.8* �6   �� 5      $��2* �6   �� 5      $��15 �6    � RowDefinitions 5   
   �� 5      $��16 �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $
��1.3* �6   �� 5      $
��0.7* �6   �� 5      $��16 �6   .+�5   
   �� $��1��6   $��JOB TYPE��6    $��Center=�63   $��1��6P   $��Center=�6]   $
��Bold"�6x   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5   
   $��1��6   $��LENGTH (Days)��6    $��Center=�68   $��2��6U   $��Center=�6b   $
��Bold"�6}   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5   
   $��1��6   $��TOTAL COST ($)��6    $��Center=�69   $��3��6V   $��Center=�6c   $
��Bold"�6~   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5   
   $��1��6   $��DESCRIPTION��6    $��Center=�66   $��4��6S   $��Center=�6`   $
��Bold"�6{   $��18(�6�   $	��145��6�   $��Right=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ ComboboxNewTypes��5      $��2��6-   $��Right=�6=   $��1��6Y   $	��140��6f   $��40��6r   $��Center=�6~   $��18(�6�   $��Center=�6�   $��0,0,10,0q�6�   .@�5      �� -   5        $��JobDrill��5      $��Drill��6+   $��15(�6;   �� 5       -   5        $��JobExcavator��5       $��	Excavator��6/   $��15(�6C   �� 5!      -   5        $��JobTruck��5!      $��Truck��6+   $��15(�6;   �� 5"      -   5        $��
JobCrusher��5"      $��Crusher��6-   $��15(�6?   �� 5#      -   5        $��JobReclaimer��5#      $��	Reclaimer��6/   $��15(�6C   �� 5$      -   5        $��JobTrain��5$      $��Train��6+   $��15(�6;   5%      �� 5&   
   -   5         ��Name$ TextboxLength��5&      $��2��6)   $��Right=�69   $��0,0,10,0q�6U   $��2��6g    �� TextWrapping$
 Wrap=�6t   $�� ��6�   $��Center=�6�   $	��140��6�   $��40��6�   $��18(�6�   $
��Bold"�6�   $��Center=�6�   $��Right=�6  �� 5'   
   -	   5        $ TextboxCost��5'      $��2��6(   $��Right=�68   $��0,0,10,0q�6T   $��3��6f   $
 Wrap=�6s   $�� ��6�   $��Center=�6�   $	��140��6�   $��40��6�   $��18(�6�   $
��Bold"�6�   $��Center=�6�   $��Right=�6  �� 5(   
   -
   5        $ TextboxDescription��5(      $��2��6.   $��Right=�6>   $��0,0,10,0q�6Z   $��4��6l   $��2��6y   $
 Wrap=�6�   $+��%Please describe the job details here.��6�   $��Center=�6�   $	��140��6�   $	��140��6�   $��13(�6   6  �� 5)   
   -   5         ��Name$ ButtonCreate��5)      $��CREATE��6'   $��2��68   $��0,0,10,0q�6H   $��6��6Z   $��38��6g   $	��140��6s   $��Center=�6�   $��Right=�6�   $��Black"�6�   $��18(�6�   ���	#FFB4F6A06�   5*      5+      