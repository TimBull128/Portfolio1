   M S B A M L   `   `   `  ���� C  ?CupofTea, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CupofTeaCupofTea      CupofTea.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:CupofTea       � Title$  
Cup of Tea��$��453.581��$	��800�� � 
ResizeMode$ NoResize=�.���  � ColumnDefinitions �� $
��190* ��� $��9* ��� $
��595* � � RowDefinitions �� $
��362* ��� $	��57* �.+��� -    ��Name$ 	TxtOutput��$
��Left=�$	��277��$��
179,10,0,0q�$
��Wrap=�$	��Top=�$	��603��{�.  $��3���� -    ��Name$ BtnReset��$��Reset��$
��Left=�$��	10,10,0,0q�$	��Top=�$��75��	� -    	�Name$ 	RadKettle��$��Kettle��$
��Left=�$��	10,51,0,0q�$	��Top=� 	� 	GroupName$ 
Containers��	� -   $ 	RadBottle��$��Milk Bottle��$
��Left=�$��	9,128,0,0q�$	��Top=�$ 
Containers��	� -   $ RadSpoon��$��Spoon��$
��Left=�$��
10,205,0,0q�$	��Top=�$ 
Containers��	� -   $ RadCup��$	��Cup��$
��Left=�$��
10,260,0,0q�$	��Top=�$ 
Containers���� -    ��Name$ TxtKetInHand��$��	In Hand: ��$
��Left=�$��	10,71,0,0q�$	��Top=�{�.  $��93���� -   $ TxtBotInHand��$��	In Hand: ��$
��Left=�$��
10,148,0,0q�$	��Top=�{�.  $��93���� -	   $ TxtSpInHand��$��	In Hand: ��$
��Left=�$��
10,225,0,0q�$	��Top=�{�.  $��93���� -
   $ BtnUp��$��Pick Up��$��Right=�$��	0,3,524,0q�$	��Top=�$��75��$��1��$��2��$��1���� -   $ BtnDown��$��Put Down��$��Right=�$��
0,30,524,0q�$	��Top=�$��75��$��1��$��1��$��2���� -   $ BtnFill��$
��Fill��$��Right=�$��	0,3,422,0q�$	��Top=�$��75��$��1��$��2���� -   $ BtnEmpty��$��Empty��$
��Left=�$��	99,31,0,0q�$	��Top=�$��75��$��1��$��2���� -   	 ��Name$	 
CheckWater��$��Water��$
��Left=�$��
14,342,0,5q�{�.  �� -   $	 	CheckMilk��$
��Milk��$
��Left=�$��14,6,0,0q�$	��Top=�{�.  $��1���� -   $	 CheckTeabag��$��Teabag��$
��Left=�$��	14,26,0,0q�$	��Top=�$��1��{�.  �� -   $	 CheckToKettle��$��	To Kettle��$
��Left=�$��	90,51,0,0q�$	��Top=��� -   $	 CheckToBottle��$��	To Bottle��$
��Left=�$��
90,128,0,0q�$	��Top=��� -   $	 CheckToSpoon��$��To Spoon��$
��Left=�$��
90,205,0,0q�$	��Top=��� -   $	 
CheckToCup��$��To Cup��$
��Left=�$��
83,260,0,0q�$	��Top=�	� -   $ RadWater��$��Water��$
��Left=�$��
90,331,0,0q�$	��Top=�$ Ingredients��	� -   $ RadMilk��$
��Milk��$
��Left=�$��
90,348,0,0q�$	��Top=�$ Ingredients��	� -   $ 	RadTeabag��$��Teabag��$
��Left=�$��90,6,0,0q�$	��Top=�$ Ingredients��$��1��	� -   $ 
RadNothing��$��Nothing��$
��Left=�$��	90,21,0,0q�$	��Top=�$ Ingredients��$��1���� -   $ TxtCupInHand��$
��Left=�$��23��$��
12,281,0,0q�
 �� TextWrapping$

 Wrap=�$��	In Hand: ��$	��Top=�$	��120�� J� RenderTransformOrigin$ 0.5,0.5&�{�.  .��z�V� .~��� �� ��  �� Angle$ 0.542Q�T� .���� -   $ 	BtnPourIn��$��Pour In��$
��Left=�$��	201,5,0,0q�$��1��$	��Top=�$��75��$��2���� -   $ 	BtnTurnOn��$��Turn On��$
��Left=�$��
200,33,0,0q�$��1��$	��Top=�$��75��$��2���� -   $ BtnStir��$
��Stir��$��2��$
��Left=�$��	297,4,0,0q�$��1��$	��Top=�$��75��{�.  �� -   $ 
BtnTakeOut��$��Take Out��$��2��$
��Left=�$��
297,32,0,0q�$��1��$	��Top=�$��75��{�.  