   M S B A M L   `   `   `  ����L  HRecuritmentSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:RecuritmentSystem20122791RecuritmentSystem20122791  5   	   0    *RecuritmentSystem20122791.ContractorWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:RecuritmentSystem20122791     5   	     � Title$  ContractorWindow��5   	   $	��500��6"   $	��700��6/   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��15 �6   �� 5      $��1* �6   �� 5      $��15 �6   �� 5      $��2* �6   �� 5      $��15 �6    � RowDefinitions 5   
   �� 5      $��16 �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��16 �6   .+�5   
   �� -   5         ��Name$ ListboxContractors��5      $��3��6.   $��1��6>   $��2��6R   $��4��6_   $��	0,10,0,10q�6p   ���	#FF000000
�������$��10(�6�   $��30,0,0,0q�6�   6�   �� 5   
   $��1��6   $��Contractor Information��6    $
��Left=�6A   $��1��6\   $��Bottom=�6i   $��70��6�   $	��660��6�   $��3��6�   $��50(�6�   $��Black"�6�   $��5,0,0,10q�6�   �� 5   
   -   5         ��Name$ 	ButtonAdd��5      $	��ADD��6%   $��1��63   $��0,5,0,10q�6C   $��2��6U   $��70��6b   $	��210��6n   $��Center=�6{   $��Center=�6�   $
��Bold"�6�   $��30(�6�   ���	#FFB4F6A06�   �� 5   
   -   5        $ ButtonTerminate��5      $��	TERMINATE��6+   $��1��6?   $��0,5,0,10q�6O   $��3��6a   $��70��6n   $	��210��6z   $��Center=�6�   $��Center=�6�   $
��Bold"�6�   $��30(�6�   ���	#FFB4F6A06�   �� 5   
   -   5        $ 
ButtonEdit��5      $
��EDIT��6&   $��1��65   $��0,5,0,10q�6E   $��4��6W   $��70��6d   $	��210��6p   $��Center=�6}   $��Center=�6�   $
��Bold"�6�   $��30(�6�   ���	#FFD92014
���������5      �� 5        �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5!      �  � Color$ Black��6#   � 5"      $ 	#FFF45A1E��6#    � Offset$ 1Q�65   5#      5%      �� 5&   
   -   5        $ ButtonDelete��5&      $��DELETE��6(   $��1��69   $��0,5,0,10q�6I   $��5��6[   $��70��6h   $	��210��6t   $��Center=�6�   $��Center=�6�   $
��Bold"�6�   $��30(�6�   ���	#FFD92014
���������5'      �� 5(      $ 0.5,1&�6&   $ 0.5,0&�67   .��5)      � $ Black��6#   � 5*      $ 	#FFF45A1E��6#   $ 1Q�65   5+      5-      5.      5/      